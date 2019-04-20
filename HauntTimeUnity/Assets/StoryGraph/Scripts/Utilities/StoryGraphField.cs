using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StoryGraph
{
    public class StoryGraphField : PropertyAttribute
    {
        private StoryDrawer propertyType;

        public StoryGraphField()
        {
            propertyType = StoryDrawer.PropertyField;
        }

        public StoryGraphField(StoryDrawer _type)
        {
            propertyType = _type;
        }

        public StoryDrawer GetPropertyType()
        {
            return propertyType;
        }
    }
}