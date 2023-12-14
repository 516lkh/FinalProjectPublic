# 🔫프로젝트 결과물 소개🔫

### 🎮︎게임 간단 소개🎮︎

크리쳐들을 총으로 사냥하면서 원하는 스킬 트리로 플레이어를 강화하고, 아이템을 수집하여 

최종적으로 보스를 사냥하는 3D 슈팅게임입니다.

- **스토리 라인 ☢︎**
    
    핵전쟁을 거치며 전투에 능숙한 사람들만이 살아남았습니다. 
    
    이제 지구에 남은 건 몇 안되는 사람, 방사능, 그리고 방사능에 변이된 크리쳐들 뿐입니다. 
    
    그러나 당신 또한 방사능에 선택받았습니다. 
    
    특별한 힘으로 크리쳐들을 물리치고 인류를 구해내야 합니다.
    

### 🕹️조작법 🕹️

![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/83c75a39-3aba-4ba4-a792-7aefe4b07895/cfb0117c-5b55-44f9-958d-843e3dba49cc/Untitled.png)

---

# 🎞︎프로젝트 개요 및 목표🎞︎

### ❗게임 프로젝트의 주요 특징과 성격❗

- **캐릭터 선택🧍**
    - 총기와 스킬이 각기 다른 캐릭터를 선택할 수 있습니다. 총기마다 조작감이 상이합니다.
    
- **몬스터 사냥**과 **아이템 파밍**을 통한 **플레이어 성장 🎁**
    - 몬스터 사냥을 통해 얻은 경험치와 아이템 파밍을 통해 캐릭터를 성장시키고, 레벨업을 통해 해금되는 스킬로 플레이어를 원하는 방향으로 성장시킬 수 있습니다.
    
- **총**과 **스킬**을 이용한 **사냥 🔥**
    - 게임이 시작되면 몬스터를 사냥할 수 있습니다.
    - 몬스터는 다양한 스킬을 사용해 플레이어에게 데미지를 입힙니다.
    - 모든 몬스터를 잡으면 보스몬스터가 등장하고, 보스몬스터까지 처치 시 게임은 끝납니다.

---

# 👨‍💻기술적인 도전 과제👨‍💻

- **절차적 애니메이션**과 **코루틴**을 사용해 **거미의 자연스러운 움직임** 구현 **🕷**
    - 적용 이유
        - 건물이 많은 맵 특성상 더 자연스럽게 런타임에서 씬에 있는 오브젝트의 위치를 확인해 거미의 발끝이 그 위에 위치하도록 하기 위해 절차적 애니메이션 적용.
        - 필요할 때만 코드블럭이 실행되는 코루틴을 사용해 구현.
        
    - 구현 방법
        - 거미의 몸이 움직일 때 어느 정도 이상 움직였다면 IK Target을 스크립트로 이동시켜 자연스러운 움직임 구현.
        - 코루틴이 자주 호출되므로 WaitForFixedUpdate를 캐싱해놓고 사용.
        
- **애니메이션 레이어, 아바타 마스크, 블렌드, 루트모션, 리깅, IK, CharacterController, FSM**을 이용해 **플레이어 움직임** 구현 ****🥷
    - 적용 이유
        - 무기별 손 위치, 바라보는 방향이 플레이어의 각 부위 각도에 반영되어야 하기때문에 리깅과 IK를 사용하여 구현
        - 플레이어가 조준방향을 바라보고있는 TPS 이므로 8방향 다리 움직임을 자연스럽게 구사하기 위해 애니메이션 블렌드와 루트모션을 활용
        - 플레이어 입력에 따라 부위별로 따로 움직일 필요가 있어 애니메이션 레이어와 아바타 마스크 활용
        - 다소 캐주얼한 게임(그리고 그래픽)이므로 점프는 유니티에서 제공하는 CharacterController를 활용
    
    - 구현 방법
        - 플레이어 모델 리깅 후 무기를 든 오른손, 허리에 Multi Aim Constraint를 적용하여 마우스 에임 방향을 보게하기
        - 플레이어 무기를 들지 않은 손은 Two Bone IK Constraint를 이용해 무기를 자연스럽게 쥐게 만들기
        - 아바타 마스크로 움직일 부위를 정하고, 애니메이션 레이어에 적용하여 오른손/상체만 특정 애니메이션을 취하게 만들기
        - FSM을 이용해 플레이어 각 상태별 스탯 업데이트나 플레이어 입력시 작동하는 메서드를 오버라이드 가능하게 만들어, 필요에 따라 루트모션으로 플레이어를 움직일지, CharacterController로 움직일지 정할수 있도록 구현
        
- **오브젝트 풀링**을 이용한 **메모리 관리 🔧**
    - 적용 이유
        - 굉장히 많이 발생하는 총알 프리팹이나 이펙트 프리팹을 일일이 생성했다가 파괴하면 사용되는 시간도 늘어나고, 메모리 단편화, GC발생이 잦아질 수 있기 때문에 적용.
        
    - 구현 방법
        - 필요한 오브젝트를 사용하고 후 파괴하는 것이 아니라 비활성화했다가 필요할 때 다시 활성화해서 사용.
        - 몬스터를 FSM으로 구현했기 때문에 다시 활성화 시 초기상태로 돌려놓기만 하면 됨
        
- **블렌더, 쉐이더 그래프**와 **비주얼 이펙트**를 통한 **스킬 이펙트** 구현 **🌪︎**
    - 적용 이유
        - 사용하는 스킬에서 강조가 필요한 부분을 개발자 임의로 조정할 수 있게 하기 위함.
        
    - 구현 방법
        - 블렌더를 통해 원하는 형태의 메쉬를 제작하고, 쉐이더 그래프를 통해 원하는 material을 구현. 필요할 경우 해당 스킬 오브젝트를 좀 더 강조하기 위해 비주얼 이펙트 사용.
        
- **인벤토리**와 **가중치**를 통한 **랜덤 아이템 드랍** 구현 **📦︎**
    - 적용 이유
        - 아이템이 로그라이크 형식으로 수집되어 플레이어를 강화하는데 사용되는데, 수집한 아이템과 효과를 확인하기 위해 인벤토리가 구현되었고, 아이템 등급을 랜덤하게 정해주기 위해 가중치로 구현하였음.
        
    - 구현 방법
        - 인벤토리
            
             각 슬롯의 아이템 데이터와 개수를 가져 각각의 아이템 툴팁이 나올 수 있게 구성한 아이템 슬롯. 아이템 슬롯의 배열을 받아 인벤토리 UI를 초기화하고, 아이템 리스트를 갖는 인벤토리 매니저로 구성하였다. 
            
        - 가중치 랜덤 아이템 구현
            
            현재는 등급 별 확률만 나뉘어 있어 세부적인 가중치는 필요하지 않지만 주요 아이템이 생기고, 밸런스를 맞춰야 할 때 각각의 아이템의 확률을 조정할 수 있도록 가중치를 활용해 랜덤 시스템을 구성.
            

---

# 사용 기술 스택

- **C#**
- **GitHub**
- **Visual Studio**
- **Unity 2022.3.2f1 (URP)**
    - Visual Effect, Shader Graph
    - Scriptable Object
    - Procedural Animation
    - FSM
    - DOTWEEN
    - Animation Rigging

---

# 클라이언트 구조

### 매니저

- 매니저 구조
    
    ![Untitled](https://prod-files-secure.s3.us-west-2.amazonaws.com/83c75a39-3aba-4ba4-a792-7aefe4b07895/2a8f89b2-a14c-4435-a99d-bb2b54806761/Untitled.png)
    
    게임 전반적으로 필요한 메소드, 데이터를 관리하는 파괴되지 않는 매니저
    
    | GameManager | 게임 전반적으로 필요한 데이터를 저장 |
    | --- | --- |
    | AudioManager | 게임 전반적으로 필요한 오디오소스, 클립을 저장해놓고 사용 |
    | ResourceManager | 게임에서 필요한 리소스 로드 및 삭제 |
    | UIManager | 씬에서 필요한 UI 로드 |
    | SceneLoadManager | 상황에 맞게 필요한 씬들을 비동기적 또는 동기적으로 로드 |
    
    각 씬마다 매니저를 설정해서 씬 로드시 자동으로 호출되어 해당 씬에 맞는 기능 및 로직 구현.
    
    | StartSceneManager | 스타트씬 생성 및  AudioManager 중지 |
    | --- | --- |
    | LoadingSceneManager | 로딩 씬 기능 구현 |
    | CharacterSelectSceneManager | 캐릭터 선택에 필요한 cinemachine카메라 세팅 및 회전 |
    | GamePlaySceneManager | 몬스터, 아이템, 플레이어 등 게임에 필요한 오브젝트 세팅 |

---

# 사용자 개선 사항

- 플레이어 스탯창의 캐릭터가 대머리로 보임
    
    → 플레이어 레이어에 일부 부속이 포함되어 있지 않아서 레이어 설정해서 해결
    
- 인벤토리 툴팁이 뜬 상태로 인벤토리를 닫을 시 툴팁이 사라지지 않음
    
    → 인벤토리 UI를 조작하는 코드에 인벤토리를 닫을 때 툴팁도 숨기게 설정
    

---

# 코드 샘플 및 주석

- ObjectPooling
    - ObjectPool
        
        
        ```
        public class ObjectPool : MonoBehaviour
        {
        	public GameObject prefab;
        	public int poolSize = 1000;
        	private List<GameObject> objects;
        	private void Awake()
        	{
        
        	}
        
        	public void Initialize(GameObject go)
        	{
        	    prefab = go;
        	    objects = new List<GameObject>();
        
        	    GameObject obj = Instantiate(go, transform);
        	    obj.SetActive(false);
        	    objects.Add(obj);
        	}
        
        	public GameObject GetObject(Vector3 position, Quaternion rotation)
        	{
        	    foreach (var obj in objects)
        	    {
        	        if (!obj.activeInHierarchy)
        	        {
        	            obj.transform.position = position;
        	            obj.transform.rotation = rotation;
        	            obj.SetActive(true);
        	            return obj;
        	        }
        	    }
        
        	    if (objects.Count < poolSize)
        	    {
        	        var ob = Instantiate(prefab, position, rotation, transform);
        	        objects.Add(ob);
        	        return ob;
        	    }
        	
        	    Debug.Log("ObjectPool Out of Range");
        	    return null;
        	}
        	
        
        	public void ReleaseObject(GameObject obj)
        	{
        	    obj.SetActive(false);
        	}
        }
        ```
        
    - ObjectPoolManager
        
        
        ```
        public class ObjectPoolManager : MonoBehaviour
        {
        	public static ObjectPoolManager Instance;
        	public List<ObjectPool> objectPools = new List<ObjectPool>();
        	
        	private void Awake()
        	{
        	    if (Instance == null)
        	    {
        	        Instance = this;
        	    }
        	    else
        	    {
        	        Destroy(gameObject);
        	        return;
        	    }
        	}
        	
        	public void CreateObjectPool(GameObject go)
        	{
        	    GameObject Obj = new GameObject(go.name+"(Clone)");
        	    Obj.transform.parent = transform;
        	    ObjectPool objPool = Obj.AddComponent<ObjectPool>();
        	    objPool.Initialize(go);
        	    objectPools.Add(objPool);
        	}
        	
        		public GameObject GetObjectFromPool(GameObject go, Vector3 position, Quaternion rotation)
        	{
        	    ObjectPool pool = objectPools.Find(p => p.name == go.name + "(Clone)");
        	    if (pool != null)
        	    {
        	        return pool.GetObject(position, rotation);
        	    }
        	    else
        	    {
        	        CreateObjectPool(go);
        	        pool = objectPools.Find(p => p.name == go.name + "(Clone)");
        	        return pool.GetObject(position, rotation);
        	    }
        	}
        	
        
        	public void ReleaseObjectToPool(GameObject go)
        	{
        	    ObjectPool pool = objectPools.Find(p => p.name == go.name);
        	    if (pool != null)
        	    {
        	        pool.ReleaseObject(go);
        	    }
        	    else
        	    {
        	        Debug.LogError("Object pool with name '" + go.name + "' not found.");
        	    }
        	}
        }	
        ```
        
- FiniteStateMachine
    - IState
        
        ```jsx
        public abstract class StateMachine
        {
        public IState previousState;
        protected IState currentState;
        
        }
        ```
        
    - StateMachine
        
        
        ```
        public abstract class StateMachine
        {
        	public IState previousState;
        	protected IState currentState;
        
        	public void ChangeState(IState newState)
        	{
        	    currentState?.Exit();
        
        	    previousState = currentState;
        
        	    currentState = newState;
        
        	    currentState?.Enter();
        	}
        
        	public void HandleInput()
        	{
        	    currentState?.HandleInput();
        	}
        
        	public void Update()
        	{
        	    currentState?.Update();
        	}
        
        	public void LateUpdate()
        	{
        	    currentState?.LateUpdate();
        	}
        
        	public void PhysicsUpdate()
        	{
        	    currentState?.PhysicsUpdate();
        	}
        }
        ```
        
- Inventory
    - InventoryManager
        
        ```jsx
        public class InventoryManager : MonoBehaviour
        {
            [SerializeField] private GameObject slotHolder;
            public static InventoryManager instance;
            public List<InventorySlot> inventoryItem = new List<InventorySlot>(21);
            private GameObject[] slots;
            public Player player { get; private set; }
            [SerializeField] private GameObject inventoryUI;
        
            private void Awake()
            {
                instance = this;     
            }
        
            private void Start()
            {
                slots = new GameObject[slotHolder.transform.childCount];
                player = GamePlaySceneManager.Instance.Player.GetComponentInChildren<Player>();
                for (int i=0;i<slotHolder.transform.childCount;i++)
                {
                    slots[i] = slotHolder.transform.GetChild(i).gameObject;
                }
                RefreshUI();
            }
        
            public void RefreshUI()
            {
                for(int i=0;i<slots.Length;i++)
                {
                    try
                    {               
                        slots[i].transform.GetComponent<Image>().enabled = true;
                        slots[i].transform.GetComponent<Image>().sprite = inventoryItem[i].GetItem().icon;
                        slots[i].transform.GetChild(0).GetComponent<Text>().text = inventoryItem[i].GetQuantity() + "";
                        slots[i].transform.GetComponent<TooltipTrigger>().SetItem(inventoryItem[i].GetItem());
                    }
                    catch
                    {
                        slots[i].transform.GetComponent<Image>().sprite = null;
                        slots[i].transform.GetComponent<Image>().enabled = false;
                        slots[i].transform.GetChild(0).GetComponent<Text>().text = "";
                    }
                }
            }
        
            public void AddItem(ItemData item)
            {
                //inventoryItem.Add(item);
                InventorySlot slot = Contains(item);
                if (slot != null)
                    slot.AddQuantity(1);
                else
                {
                    inventoryItem.Add(new InventorySlot(item, 1));
                }
        
                RefreshUI();
            }
        
            public void ClearInventory()
            {
                inventoryItem.Clear();
            }
        
            public InventorySlot Contains(ItemData item)
            {
                foreach (InventorySlot slot in inventoryItem)
                {
                    if(slot.GetItem() == item) return slot;
                }
                return null;
            }
        
            public void InventoryUI()
            {
                AudioManager.Instance.PlayInventorySFX();
                if (inventoryUI.activeSelf)
                {
                    inventoryUI.SetActive(false);
                    Cursor.visible = false; // 커서를 숨김
                    Cursor.lockState = CursorLockMode.Locked;
                    TooltipSystem.Hide();
                }         
                else
                {
                    inventoryUI.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        ```
        
    - InventorySlot
        
        ```jsx
        [System.Serializable]
        public class InventorySlot
        {
           public ItemData item;
           [SerializeField] private int quantity;
        
            public InventorySlot()
            {
                item = null;
                quantity = 0;
            }
            public InventorySlot(ItemData _item, int _quantity)
            {
                item = _item;
                quantity = _quantity;
            }
            public ItemData GetItem() { return item; }
            public int GetQuantity() { return quantity; }
            public void AddQuantity(int _quantity) { quantity += _quantity; }   
        }
        ```
        
- Skill 구성 요소
    
    Skill.cs
    
    ```jsx
    using UnityEngine;
    
    public abstract class Skill : MonoBehaviour
    {
    
        public abstract void ActiveSkill();
    
        public abstract void SetSkillData(SkillSO skillData);
    
    }
    ```
    
    해당 추상 클래스를 상속받아 만든 클래스 예시(즉시 체력회복 기능 가진 스킬)
    
    InstantHeal.cs
    
    ```jsx
    using System.Collections;
    using UnityEngine;
    
    public class InstantHeal : Skill
    {
        private float skillTime;
        private float playerHealthPointRecovery;
    
        public override void SetSkillData(SkillSO skillData)
        {
            skillTime = skillData.skillTime;
            playerHealthPointRecovery = skillData.playerHealthPointRecovery;
    
            ActiveSkill();
        }
    
        public override void ActiveSkill()
        {
            StartCoroutine(StartSkillActivation(skillTime));
        }
    
        IEnumerator StartSkillActivation(float skillTime)
        {
            
            while (skillTime >= 1.0f)
            {
                skillTime -= 1f;
                SkillManager.Instance.player._playerConditions.Heal(playerHealthPointRecovery * SkillManager.Instance.player._playerConditions.health.maxValue);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
    ```
    
- 씬 관리 구조
    - SceneLoadManager
        
        ```jsx
        public enum Scenes
        {
            Unknown,
            StartScene,
            CharacterSelectScene,
            GamePlayScene,
            LoadingScene,
            GameOverScene,
            EndingCredit,
            YCYDevScene
        }
        
        public class SceneLoadManager : Singleton<SceneLoadManager>
        {
            private BaseSceneManager _curSceneManager;
            private Scenes _curSceneType = Scenes.StartScene; 
            private Scenes _nextSceneType = Scenes.Unknown; // 현재 Scene이 LoadingScene일 경우 다음에 호출 될 Scene
        
            private void Start()
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        
            public void Initialized(BaseSceneManager sceneManager)
            {
                _curSceneManager = sceneManager;
            }
        
            void OnSceneLoaded(Scene scene, LoadSceneMode mode)
            {
                if (scene.name == _curSceneType.ToString())
                {
                    LoadScenesManager();
                }
            }
        
            public void LoadScene(Scenes sceneType, Scenes nextSceneType = Scenes.Unknown)
            {
                if(_curSceneManager != null)
                {
                    _curSceneManager.Clear();
                }
        
                _curSceneType = sceneType;
                if (_curSceneType == Scenes.LoadingScene) _nextSceneType = nextSceneType;
                SceneManager.LoadScene(_curSceneType.ToString());
            }
        
            public AsyncOperation LoadSceneAsync()
            {
                if (_curSceneManager != null)
                {
                    _curSceneManager.Clear();
                }
        
                _curSceneType = _nextSceneType;
                return SceneManager.LoadSceneAsync(_nextSceneType.ToString());
            }
        
            public void LoadScenesManager()
            {
                BaseSceneManager go = FindObjectOfType<BaseSceneManager>();
                GameObject managerObj;
                if (go != null)
                {
                    managerObj = go.gameObject;
                }
                else
                {
                    managerObj = Instantiate(ResourceManager.Instance.LoadResource<GameObject>($"Prefabs/Managers/ScenesManager/{_curSceneType.ToString()}Manager"));
                }
                _curSceneManager = managerObj.GetComponent<BaseSceneManager>();
            }
        }
        ```
        
    - BaseSceneManager
        
        ```jsx
        public class BaseSceneManager : MonoBehaviour
        {
            public Scenes SceneType = Scenes.Unknown;
        
            protected bool _init = false;
        
            private void Start()
            {
                Init();
            }
        
            protected virtual bool Init()
            {
                if (_init) return false;
        
                _init = true;
                Object go = FindObjectOfType(typeof(EventSystem));
                if (go == null) Instantiate(Resources.Load("Prefabs/UI/EventSystem"));
        
                return true;
            }
        
            public virtual void Clear() { }
        }
        ```
        
        ```jsx
        public class StartSceneManager : BaseSceneManager
        {
            private static StartSceneManager _instance;
        
            public static StartSceneManager Instance
            {
                get
                {
                    if (_instance != null) { return _instance; }
        
                    _instance = FindObjectOfType<StartSceneManager>();
                    if (_instance != null) { return _instance; }
        
                    _instance = new GameObject(nameof(StartSceneManager)).AddComponent<StartSceneManager>();
                    return _instance;
                }
            }
        
            protected override bool Init()
            {
                if (!base.Init()) return false;
        
                SceneType = Scenes.StartScene;
                UIManager.Instance.ShowSceneUI<UI_StartScene>();
                AudioManager.Instance.PlayMusic("MainSceneTheme");
        
                return true;
            }
        
            public override void Clear()
            {
                AudioManager.Instance.StopMusic();
            }
        }
        ```
        

---

# 트러블 슈팅

| 플레이어 카메라 혼동 문제 :
플레이어가 이동할때와 가만 있을때 카메라 위치가 부자연스럽게 바뀌는 현상 | 각 State간 필드는 State 끼리 공유하지 않아서 발생한 문제. 카메라 관련 필드를 State 말고 StateMachine / Player 에게 할당하여 각 State들이 공유 |
| --- | --- |
| 탄환이 콜라이더에 부딪혔을때 충돌 이펙트가 충돌체 법선방향으로 생성 안되는 문제 | OnTrigerEnter() 로 받아오는 collider는 ‘충돌 위치 정보’ 없이 ‘충돌 했는지’와 ‘충돌 오브젝트 정보’만 갖기때문에 법선벡터를 구할 수 없음.
하지만 탄환은 trigger 충돌해야 하므로 탄환 충돌시 탄환의 살짝 뒤에서 레이캐스트를 쏘아 충돌 위치 정보(collision)를 갖고 법선벡터방향을 구한뒤 이펙트 적용 |
| SkillManager에서 Scriptable Object 데이터를 받아와서 Get함수로 해당 데이터를 다른 곳에서 범용적으로 쓰려고 했지만 인식되지 않는 문제 | Scriptable Object는 참조 메모리 형식이어서 해당 메모리를 두 번 참조해서 정확한 데이터를 가져올 수 없음. 해결하기 위해 SkillManager에서 메모리를 할당해 값을 초기화해 사용하려고 했지만 추가 메모리 할당과 Scriptable Object 사용하는 이유가 희미해지기 때문에 해당 Scriptable Object 배열을 선언하지 않고 바로 해당 메모리에서 값을 리턴하는 것으로 해결. |
| 아이템이 수집되지 않는 문제 :  인벤토리가 플레이어 프로퍼티를 제대로 받아오지 못하는 문제 | 매니저에서 선언된 Player객체가 PlayerPack이고, Player스크립트가 PlayerPack하위 객체인 Player에 있었기 때문에 GetComponentInChildren으로 받아 해결 |
| 스킬트리에서 스킬을 해금할 때 레벨 순서대로 활성화시키지 않은 경우 스킬 발동 키가 달라지는 문제 | 활성화시킨 스킬을 리스트로 저장해서 문제가 생김. 스킬을 해금할 때 딕셔너리에 저장해 key는 스킬을 사용할 키의 이름, value는 해당 스킬 데이터를 담는 방식으로 자료구조를 변경해서 해결 |
|  |  |

---

---

---

### 팀원 소개
