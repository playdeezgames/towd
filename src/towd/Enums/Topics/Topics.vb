Imports System.Runtime.CompilerServices
Imports towd.business
Imports towd.data

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
            New VerbTypeTopicDescriptor(Topic.VerbTypeWait, VerbType.Wait),
            New ItemTypeTopicDescriptor(Topic.ItemTypePlantFiber, ItemType.PlantFiber),
            New ItemTypeTopicDescriptor(Topic.ItemTypeStick, ItemType.Stick),
            New ItemTypeTopicDescriptor(Topic.ItemTypeRock, ItemType.Rock),
            New ItemTypeTopicDescriptor(Topic.ItemTypeTwine, ItemType.Twine),
            New ItemTypeTopicDescriptor(Topic.ItemTypeSharpRock, ItemType.SharpRock),
            New ItemTypeTopicDescriptor(Topic.ItemTypeHatchet, ItemType.Hatchet),
            New ItemTypeTopicDescriptor(Topic.ItemTypeLog, ItemType.Log),
            New ItemTypeTopicDescriptor(Topic.ItemTypeHammer, ItemType.Hammer),
            New ItemTypeTopicDescriptor(Topic.ItemTypePlank, ItemType.Plank),
            New ItemTypeTopicDescriptor(Topic.ItemTypeSharpStick, ItemType.SharpStick),
            New ItemTypeTopicDescriptor(Topic.ItemTypeGrub, ItemType.Grub),
            New ItemTypeTopicDescriptor(Topic.ItemTypeCookingFire, ItemType.CookingFire),
            New ItemTypeTopicDescriptor(Topic.ItemTypeCookedGrub, ItemType.CookedGrub),
            New ItemTypeTopicDescriptor(Topic.ItemTypeClay, ItemType.Clay),
            New ItemTypeTopicDescriptor(Topic.ItemTypeCharcoal, ItemType.Charcoal),
            New ItemTypeTopicDescriptor(Topic.ItemTypeUnfiredBrick, ItemType.UnfiredBrick),
            New ItemTypeTopicDescriptor(Topic.ItemTypeBrick, ItemType.Brick),
            New ItemTypeTopicDescriptor(Topic.ItemTypeFishingNet, ItemType.FishingNet),
            New ItemTypeTopicDescriptor(Topic.ItemTypeRawFish, ItemType.RawFish),
            New ItemTypeTopicDescriptor(Topic.ItemTypeRawFishFilet, ItemType.RawFishFilet),
            New ItemTypeTopicDescriptor(Topic.ItemTypeFishHead, ItemType.FishHead),
            New ItemTypeTopicDescriptor(Topic.ItemTypeFishGuts, ItemType.FishGuts),
            New ItemTypeTopicDescriptor(Topic.ItemTypeKnife, ItemType.Knife),
            New ItemTypeTopicDescriptor(Topic.ItemTypeBlade, ItemType.Blade),
            New ItemTypeTopicDescriptor(Topic.ItemTypeCookedFishFilet, ItemType.CookedFishFilet),
            New ItemTypeTopicDescriptor(Topic.ItemTypeFurnace, ItemType.Furnace)
        }.ToDictionary(Function(x) x.Topic, Function(x) x)
    <Extension>
    Public Function ToDescriptor(topic As Topic) As ITopic
        Return Descriptors(topic)
    End Function

End Module
