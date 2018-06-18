using BattleTech.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace nl.flukeyfiddler.bt.Utils.Examples
{
    class GenericPopup
    {
        void SomeMethod()
        {
            GenericPopupBuilder.Create(GenericPopupType.Info, "No saved games found").AddButton("Ok", delegate
            {
                this.ReceiveButtonPress("cancel");
            }, true, null).IsNestedPopupWithBuiltInFader().Render();
        }
    }
}
