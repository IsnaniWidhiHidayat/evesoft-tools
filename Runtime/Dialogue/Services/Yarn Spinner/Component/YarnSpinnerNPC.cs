#if ODIN_INSPECTOR && YARN_SPINNER
using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading.Tasks;

namespace Evesoft.Dialogue.YarnSpinner.Component
{
    [HideMonoScript]
    [AddComponentMenu(Menu.Dialogue + "/" + nameof(YarnSpinner) + "/" + nameof(YarnSpinnerNPC))]
    [DisallowMultipleComponent]
    internal class YarnSpinnerNPC : SerializedMonoBehaviour,iNPC
    {
        #region const
        const string grpConfig = "Config";
        #endregion

        #region field
        [SerializeField,Required,HideLabel,SuffixLabel("Talk Node",true),FoldoutGroup(grpConfig)]
        private string _node;

        private bool showScript => !_node.IsNullOrEmpty();
        [SerializeField,Required,HideLabel,ShowIf(nameof(showScript)),FoldoutGroup(grpConfig)]
        private YarnProgram _script;
        #endregion

        #region property
        public string node{ get => _node; set => _node = value;}
        public YarnProgram script {get => _script; set => _script = value;}
        #endregion

        private void Start()
        {
            Init();
        }
        private async void Init()
        {
            if(_script.IsNull())
                return;

            var dialogue = await GetDialogue();
            var data     = DialogueDataFactory.CreateYarnSpinnerData();
            data.AddScript(script); 
            dialogue.Add(data);
        }
    
        private async Task<iDialogue> GetDialogue()
        {
            await new WaitUntil(()=> !DialogueFactory.Get(DialogueType.YarnSpinner).IsNull());
            return DialogueFactory.Get(DialogueType.YarnSpinner);
        }  


        #region iNPC
        [Button]
        public void Talk()
        {
            var dialogue = DialogueFactory.Get(DialogueType.YarnSpinner);
            if(dialogue.IsNull())
            {
                "Dialogue Is Not Created Please Create First with DialogueFactory.Create()".LogError();
                return;
            }

            if(_node.IsNullOrEmpty())
            {
                $"{gameObject.name} - {nameof(YarnSpinnerNPC)} - node is empty ".Log();
                return;
            }
                

            dialogue.StartDialogue(_node);
        }
        #endregion
    }
}
#endif