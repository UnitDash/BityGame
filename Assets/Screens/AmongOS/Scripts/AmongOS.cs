using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
public class AmongOS : MonoBehaviour
{
    public static AmongOS Instance;
    public AudioSource Source;
    public Camera ScreenCamera;
    public static Camera StaticScreenCamera;
    public Camera CasterCamera;
    public static Camera StaticCasterCamera;

    public App[] Applications;
    public OSComposants AmongOSComposants;
    public bool LoggedIn;
    private bool ApplicationsReady;

    public GameObject DarkScreen;
    public VideoPlayer RevealVideo;
    [System.Serializable]
    public struct OSComposants{
        public Object ApplicationIcon;
        public Vector3 AppIconOffset;
        public Object ApplicationName;
        public Vector3 AppNameOffset;
        public Object ApplicationSelect;
        public Vector3 AppSelectOffset;
        public GameObject StartMenu;
        public GameObject Clock;
        public GameObject Desktop;
        public GameObject Login;
    }
    
    [System.Serializable]
    public struct App{
        public string AppName;
        public GameObject TargetApp;

        public Vector3 AppPosition;

        [Header("ApplicationComposants")]
        public GameObject ApplicationIcon;
        public GameObject ApplicationName;
        public GameObject ApplicationSelect;

        [Header("AppStats")]
        public bool Selected;
        [Header("OpenApp")]
        public Object OpenAppPrefab;
        public Vector3 OpenPosition;
        public GameObject OpenedApp;
    }
    public void Awake()
    {
        Instance = this;
    }
    public void Start(){

        ScreenCamera = CameraController.Instance.MainCamera;
        Source = CameraController.Instance.CameraSource;
        StaticScreenCamera = ScreenCamera;
        StaticCasterCamera = CasterCamera;

        StartCoroutine(IAmongOS());
    }
    public void Update() {
        if(ApplicationsReady)
            UpdateAmongOS();
        
    }

    public IEnumerator IAmongOS()
    {
        ApplicationsReady = false;
        AmongOSComposants.Desktop.SetActive(false);
        AmongOSComposants.Login.SetActive(true);
        while(!LoggedIn)
            yield return null;
        
        AmongOSComposants.Desktop.SetActive(true);
        AmongOSComposants.Login.SetActive(false);
        
        SetupApplications();
    }

    public App CreateApplication(App PreviousApp){
        GameObject Application = new GameObject(PreviousApp.AppName);
        Application.transform.position = this.transform.position + PreviousApp.AppPosition;

        GameObject ApplicationIcon = (GameObject)Instantiate(AmongOSComposants.ApplicationIcon,Vector3.zero,Quaternion.identity,Application.transform);
        ApplicationIcon.transform.localPosition = AmongOSComposants.AppIconOffset;
        ApplicationIcon.name = "ApplicationIcon";

        GameObject ApplicationName = (GameObject)Instantiate(AmongOSComposants.ApplicationName,Vector3.zero,Quaternion.identity,Application.transform);
        ApplicationName.transform.localPosition = AmongOSComposants.AppNameOffset;
        ApplicationName.name = "ApplicationName";
        ApplicationName.GetComponent<TMPro.TextMeshPro>().text = PreviousApp.AppName;

        GameObject ApplicationSelect = (GameObject)Instantiate(AmongOSComposants.ApplicationSelect,Vector3.zero,Quaternion.identity,Application.transform);
        ApplicationSelect.transform.localPosition = AmongOSComposants.AppSelectOffset;
        ApplicationSelect.name = "ApplicationSelect";


        //Generate App
        App GeneratedApp = new App();
        GeneratedApp = PreviousApp;
        GeneratedApp.TargetApp = Application;
        GeneratedApp.ApplicationIcon = ApplicationIcon;
        GeneratedApp.ApplicationName = ApplicationName;
        GeneratedApp.ApplicationSelect = ApplicationSelect;

        Application.transform.SetParent(this.transform);
        //OpenApp
        return GeneratedApp;
    }

    public void SetupApplications(){

        if(Applications.Length > 0){
            for(int i=0;i<Applications.Length;i++){
                Applications[i] = CreateApplication(Applications[i]);
            }
        }
        ApplicationsReady = true;
    }

    public void UpdateAmongOS(){
        //Uodate Applications
        Transform RayCasterTransform = ScreenRayCaster().transform;

        if(Applications.Length > 0){
            for(int i =0;i<Applications.Length;i++){
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if(RayCasterTransform == Applications[i].ApplicationSelect.transform){
                        Applications[i].Selected = true;
                        clicks ++;
                        StartCoroutine(CheckForOpen(i,Time.time));
                    }
                    else{
                        Applications[i].Selected = false;
                    }

                }

                if(Applications[i].Selected)
                    Applications[i].ApplicationSelect.GetComponent<SpriteRenderer>().enabled = true;
                else
                    Applications[i].ApplicationSelect.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        //Update Start Menu
    
        AmongOSButton[] OSButtons = FindObjectsOfType<AmongOSButton>();
            if(Input.GetKey(KeyCode.Mouse0) && RayCasterTransform != null && RayCasterTransform.GetComponent<AmongOSButton>())
                RayCasterTransform.GetComponent<AmongOSButton>().Clicked();
            else{
                if(Input.GetKeyUp(KeyCode.Mouse0) && RayCasterTransform != null && RayCasterTransform.GetComponent<AmongOSButton>() && RayCasterTransform.GetComponent<AmongOSButton>().IsClicked)
                    RayCasterTransform.GetComponent<AmongOSButton>().OnClickEnd();
                
                for(int i =0;i<OSButtons.Length;i++){
                    OSButtons[i].NotClicked();
                }
            }
        
        

        //UpdateClock
        AmongOSComposants.Clock.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = System.DateTime.Now.Hour.ToString() + " : " + System.DateTime.Now.Minute.ToString();
    }

    private int clicks = 0;
    private bool AlowCheck = true;
    IEnumerator CheckForOpen(int app,float FirstClickTime ){
        AlowCheck = false;
        yield return new WaitForSeconds(0.01f);
        while(Time.time < FirstClickTime + 0.2f){
            if(clicks == 2){
                OpenApp(app);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        clicks = 0;
        AlowCheck = true;
    }
    public void OpenApp(int app){
        if(Applications[app].OpenedApp == null){
            GameObject OpenedApp = (GameObject)Instantiate(Applications[app].OpenAppPrefab,Vector3.zero,Quaternion.identity,this.transform);
            OpenedApp.transform.localPosition = Applications[app].OpenPosition;
            Applications[app].OpenedApp = OpenedApp;
        }
    }

    public static RaycastHit2D ScreenRayCaster(){
        RaycastHit Screenhit;
        Ray ScreenRay = StaticScreenCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D CastingHit = new RaycastHit2D();
        if(Physics.Raycast(ScreenRay,out Screenhit)){
            Vector3 CasterMax = new Vector3(StaticCasterCamera.transform.position.x + StaticCasterCamera.orthographicSize * StaticCasterCamera.aspect,StaticCasterCamera.transform.position.y + StaticCasterCamera.orthographicSize,StaticCasterCamera.transform.position.z);
            Vector3 CasterMin = new Vector3(StaticCasterCamera.transform.position.x - StaticCasterCamera.orthographicSize * StaticCasterCamera.aspect,StaticCasterCamera.transform.position.y - StaticCasterCamera.orthographicSize,StaticCasterCamera.transform.position.z);
            Vector3 CasterPosition = new Vector3(Mathf.Lerp(CasterMin.x,CasterMax.x,Screenhit.textureCoord.x),Mathf.Lerp(CasterMin.y,CasterMax.y,Screenhit.textureCoord.y),StaticCasterCamera.transform.position.z);
            CastingHit = Physics2D.Raycast(CasterPosition, Vector3.forward);
            
        }
        return CastingHit;
    }

}