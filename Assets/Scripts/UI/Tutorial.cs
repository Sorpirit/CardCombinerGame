using UI.PopupMessageHelper;
using UnityEngine;

namespace UI
{
    public class Tutorial : MonoBehaviour
    {
        /*
         *
         * +Hey stranger, Big Corporation War is soon so we need your help with buttle ship construction
+So you can see the factory assembly line at the bottom of the screen. There is only one component now but you can drag components from storage
+Good! The sad fact because of construction error, you would have to throw away one component to get new components in storage.
+Bye nice component we will miss you. As you can see they have delivered new components.
+But wait before garbing new components. To build a nice working ship you would have to follow a blueprint provided by our amazing underpaid engineers. But you are special of course we would pay you.
+ You can open the list of available blueprints on top of the screen
+ So as you can some icons are highlighted there. It means that you have that component on the assembly line. When you would have all the components from the row highlighted, the factory line would auto assemble them in a specific ship. Ahhh they can make an auto assembly line, but they can’t make a system without throwing out so expensive components. Engineers.
+ As you can guess bigger combinations produce bigger ships. Hmm simple yeah? So that what we want from you gigantic ship understand? Simple
+ And finally this cycle would repeat until you would use all components factory has produced
+ Probably you are curious about the numbers on the components. It is a component serial number or how many of them the factory has produced as you wish. So be thoughtful about them we wouldn’t give you more
+ So start working son you have many ships to build

         */
        

        [SerializeField] private PopupMessageShooter popupShoot;
        [SerializeField] private UIController controller;
        private string popupTtile = "Ttile";
        private string content = "";

        private string tutorialLb = "tutorial";
        
        private void Start()
        {
            if(PlayerPrefs.HasKey(tutorialLb))
                return;
            
            PlayerPrefs.SetInt(tutorialLb,1);
                
            popupShoot.ShootQuestionPopup(Vector3.zero, "Tutorial?","Hey stranger, Big Corporation War is soon so we need your help with buttle ship construction",
                (response) =>
                {
                    if (response)
                    {
                        StartTutorial();
                    }
                },"Amm what?(Tutorial)","I know what to do(Skip tutorial)",popupShoot.BasicScalePopup);
        }

        private void StartTutorial()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage();
            
            var s = popupShoot.ShootTriggerPopup(Vector3.up * 300, "Drag&Drop","So you can see the factory assembly line at the bottom of the screen. There is only one component now but you can drag components from storage",events);
            controller.OnAddCardToHand += (f) => s.Close();
        }

        private void NextMessage()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage2();
            
            var s= popupShoot.ShootTriggerPopup(Vector3.down * 300, "Drop components(","So you can see the factory assembly line at the bottom of the screen. There is only one component now but you can drag components from storage",events);
            controller.OnCardDroped += (f) => s.Close();
        }

        private void NextMessage2()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage3();
            popupShoot.ShootPopup(Vector3.up * 300, "New Delivery","Bye nice component we will miss you. As you can see they have delivered new components.",events);
        }

        private void NextMessage3()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage4();
            popupShoot.ShootPopup(Vector3.right* 30, "Blueprints?","But wait before garbing new components. To build a nice working ship you would have to follow a blueprint provided by our amazing underpaid engineers. But you are special of course we would pay you.",events);
        }
        
        private void NextMessage4()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage5();
            popupShoot.ShootPopup(Vector3.zero, "Hmmm what do we have here","You can open the list of available blueprints on top of the screen",events);
        }
        
        private void NextMessage5()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage6();
            popupShoot.ShootPopup(Vector3.zero, "Highlight","So as you can some icons are highlighted there. It means that you have that component on the assembly line",events);
        }
        
        private void NextMessage6()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage7();
            popupShoot.ShootPopup(Vector3.zero, "Blueprints?","When you would have all the components from the row highlighted, the factory line would auto assemble them in a specific ship. Ahhh they can make an auto assembly line, but they can’t make a system without throwing out so expensive components. Engineers.",events);
        }
        
        private void NextMessage7()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage8();
            popupShoot.ShootPopup(Vector3.zero, "High risk)","As you can guess bigger combinations produce bigger ships. Hmm simple yeah? So that what we want from you gigantic ship understand? Simple",events);
        }
        
        private void NextMessage8()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage9();
            popupShoot.ShootPopup(Vector3.zero, "Production","And finally this cycle would repeat until you would use all components factory has produced",events);
        }
        
        private void NextMessage9()
        {
            var events = popupShoot.BasicScalePopup;
            events.OnCloseComplete += (s) => NextMessage10();
            popupShoot.ShootPopup(Vector3.up * 300, "Cards info","Probably you are curious about the numbers on the components. It is a component serial number or how many of them the factory has produced as you wish. So be thoughtful about them we wouldn’t give you more",events);
        }
        
        private void NextMessage10()
        {
            var events = popupShoot.BasicScalePopup;
            //events.OnCloseComplete += (s) => NextMessage11();
            popupShoot.ShootPopup(Vector3.zero, "Good luck","So start working son you have many ships to build",events);
        }
    }
}