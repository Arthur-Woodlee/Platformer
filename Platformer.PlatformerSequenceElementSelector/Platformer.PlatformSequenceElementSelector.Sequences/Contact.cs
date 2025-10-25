using System.Collections.Generic;

namespace Platformer.PlatformerSequenceElementSelector.Sequences
{
    public class Contact
    {
        public bool NewPositionAllowed;
        public LinkedList<string> SceneObjectID;
        public Contact(bool NewPositionAllowed, LinkedList<string> SceneObjectID)
        {
            this.NewPositionAllowed = NewPositionAllowed;
            this.SceneObjectID = SceneObjectID;
        }
    }
}