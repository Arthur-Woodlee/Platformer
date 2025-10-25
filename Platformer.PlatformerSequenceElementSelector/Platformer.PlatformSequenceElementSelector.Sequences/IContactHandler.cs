using Platformer.Positioning;
using Platformer.PlatformerSceneEngine;
using System.Collections.Generic;

namespace Platformer.PlatformerSequenceElementSelector.Sequences
{
    public interface IContactHandler
{
    LinkedList<Contact> MakeContact(PlatformerRectangle boundingBox, ISceneObject inquirer, bool vacate);
}
}