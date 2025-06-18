Imports System.Runtime.CompilerServices
Imports towd.business

Module Topics
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of Topic, ITopic) =
        New List(Of ITopic) From
        {
            New TopicDescriptor(Topic.NavigationDeeds, "Deeds", "Your tale grows with every scrape and scavenge. 
Review your triumphs and failures, etched in the dust of TOWD."),
            New TopicDescriptor(Topic.NavigationGameMenu, "Game Menu", "The Wastes wait for no one. 
Save your journey, tweak settings, or abandon it all. 
Your fate hangs on this choice."),
            New TopicDescriptor(Topic.NavigationInventory, "Inventory", "Peek into your ragged pack. 
Bits of grass, a rusty tool, maybe a snack if you’re lucky. 
Manage your hoard or risk losing it all."),
            New TopicDescriptor(Topic.NavigationMove, "Movement", "Stagger into the unknown—north, south, east, or west. 
Each step could lead to salvation or a thorn in your boot. 
Pick a direction, brave soul."),
            New TopicDescriptor(Topic.NavigationSkills, "Skills", "Hone your craft or weep in weakness. 
Boost your foraging, digging, or chopping—every point could mean life or lunch."),
            New TopicDescriptor(Topic.NavigationVerb, "Verbs", "Actions speak louder than groans. 
Forage, dig, chop—choose your labor and test your skills against the wild’s whims."),
            New VerbTypeTopicDescriptor(Topic.VerbTypeAddFuel, VerbType.AddFuel),
            New VerbTypeTopicDescriptor(Topic.VerbTypeChop, VerbType.Chop),
            New VerbTypeTopicDescriptor(Topic.VerbTypeCraft, VerbType.Craft),
            New VerbTypeTopicDescriptor(Topic.VerbTypeDig, VerbType.Dig),
            New VerbTypeTopicDescriptor(Topic.VerbTypeEatFish, VerbType.EatFish),
            New VerbTypeTopicDescriptor(Topic.VerbTypeEatGrub, VerbType.EatGrub),
            New VerbTypeTopicDescriptor(Topic.VerbTypeFish, VerbType.Fish),
            New VerbTypeTopicDescriptor(Topic.VerbTypeForage, VerbType.Forage),
            New VerbTypeTopicDescriptor(Topic.VerbTypeWait, VerbType.Wait)
        }.ToDictionary(Function(x) x.Topic, Function(x) x)
    <Extension>
    Public Function ToDescriptor(topic As Topic) As ITopic
        Return Descriptors(topic)
    End Function

End Module
