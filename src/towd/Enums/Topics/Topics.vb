Imports System.Runtime.CompilerServices
Imports towd.business
Imports towd.data

Public Module Topics
    Public ReadOnly SkillTypeTopicTable As IReadOnlyDictionary(Of SkillType, Topic) =
        New Dictionary(Of SkillType, Topic) From
        {
            {SkillType.Foraging, Topic.SkillTypeForaging},
            {SkillType.Dig, Topic.SkillTypeDig},
            {SkillType.Knapping, Topic.SkillTypeKnapping},
            {SkillType.Chop, Topic.SkillTypeChop},
            {SkillType.Fish, Topic.SkillTypeFish}
        }
    Public ReadOnly VerbTypeTopicTable As IReadOnlyDictionary(Of VerbType, Topic) =
        New Dictionary(Of VerbType, Topic) From
        {
            {VerbType.Twine, Topic.VerbTypeTwine},
            {VerbType.SharpRock, Topic.VerbTypeSharpRock},
            {VerbType.SharpStick, Topic.VerbTypeSharpStick},
            {VerbType.Hatchet, Topic.VerbTypeHatchet},
            {VerbType.Hammer, Topic.VerbTypeHammer},
            {VerbType.Plank, Topic.VerbTypePlank},
            {VerbType.CookingFire, Topic.VerbTypeCookingFire},
            {VerbType.Furnace, Topic.VerbTypeFurnace},
            {VerbType.CharcoalCookingFire, Topic.VerbTypeCharcoalCookingFire},
            {VerbType.CookedGrub, Topic.VerbTypeCookedGrub},
            {VerbType.CookedFishFilet, Topic.VerbTypeCookedFishFilet},
            {VerbType.UnfiredBrick, Topic.VerbTypeUnfiredBrick},
            {VerbType.Knife, Topic.VerbTypeKnife},
            {VerbType.Blade, Topic.VerbTypeBlade},
            {VerbType.RawFishFilet, Topic.VerbTypeRawFishFilet},
            {VerbType.FishingNet, Topic.VerbTypeFishingNet},
            {VerbType.Brick, Topic.VerbTypeBrick},
            {VerbType.ForageGrass, Topic.VerbTypeForageGrass},
            {VerbType.ForagePine, Topic.VerbTypeForagePine},
            {VerbType.ForageRock, Topic.VerbTypeForageRock},
            {VerbType.Chop, Topic.VerbTypeChop},
            {VerbType.DigPond, Topic.VerbTypeDigPond},
            {VerbType.DigGrass, Topic.VerbTypeDigGrass},
            {VerbType.Wait, Topic.VerbTypeWait},
            {VerbType.Fish, Topic.VerbTypeFish},
            {VerbType.EatCookedFishFilet, Topic.VerbTypeEatCookedFishFilet},
            {VerbType.EatCookedGrub, Topic.VerbTypeEatCookedGrub},
            {VerbType.AddFuelCookingFireCharcoal, Topic.VerbTypeAddFuelCookingFireCharcoal},
            {VerbType.AddFuelCookingFireLog, Topic.VerbTypeAddFuelCookingFireLog},
            {VerbType.AddFuelCookingFirePlank, Topic.VerbTypeAddFuelCookingFirePlank},
            {VerbType.AddFuelCookingFireStick, Topic.VerbTypeAddFuelCookingFireStick},
            {VerbType.AddFuelFurnaceCharcoal, Topic.VerbTypeAddFuelFurnaceCharcoal}
        }
    Public ReadOnly ItemTypeTopicTable As IReadOnlyDictionary(Of ItemType, Topic) =
        New Dictionary(Of ItemType, Topic) From
        {
            {ItemType.PlantFiber, Topic.ItemTypePlantFiber},
            {ItemType.Stick, Topic.ItemTypeStick},
            {ItemType.Rock, Topic.ItemTypeRock},
            {ItemType.Twine, Topic.ItemTypeTwine},
            {ItemType.SharpRock, Topic.ItemTypeSharpRock},
            {ItemType.Hatchet, Topic.ItemTypeHatchet},
            {ItemType.Log, Topic.ItemTypeLog},
            {ItemType.Hammer, Topic.ItemTypeHammer},
            {ItemType.Plank, Topic.ItemTypePlank},
            {ItemType.SharpStick, Topic.ItemTypeSharpStick},
            {ItemType.Grub, Topic.ItemTypeGrub},
            {ItemType.CookedGrub, Topic.ItemTypeCookedGrub},
            {ItemType.Clay, Topic.ItemTypeClay},
            {ItemType.Charcoal, Topic.ItemTypeCharcoal},
            {ItemType.UnfiredBrick, Topic.ItemTypeUnfiredBrick},
            {ItemType.Brick, Topic.ItemTypeBrick},
            {ItemType.FishingNet, Topic.ItemTypeFishingNet},
            {ItemType.RawFish, Topic.ItemTypeRawFish},
            {ItemType.RawFishFilet, Topic.ItemTypeRawFishFilet},
            {ItemType.FishHead, Topic.ItemTypeFishHead},
            {ItemType.FishGuts, Topic.ItemTypeFishGuts},
            {ItemType.Knife, Topic.ItemTypeKnife},
            {ItemType.Blade, Topic.ItemTypeBlade},
            {ItemType.CookedFishFilet, Topic.ItemTypeCookedFishFilet}
        }
    Private Function CreateDescriptors() As IReadOnlyDictionary(Of Topic, ITopic)
        Dim topicTable = New List(Of ITopic) From
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
            New TopicDescriptor(Topic.NavigationCraft, "Verbs", "Actions speak louder than groans. 
Forage, dig, chop—choose your labor and test your skills against the wild’s whims.")
        }.ToDictionary(Function(x) x.Topic, Function(x) x)
        For Each entry In ItemTypeTopicTable
            topicTable.Add(entry.Value, New ItemTypeTopicDescriptor(entry.Value, entry.Key))
        Next
        For Each entry In VerbTypeTopicTable
            topicTable.Add(entry.Value, New VerbTypeTopicDescriptor(entry.Value, entry.Key))
        Next
        For Each entry In SkillTypeTopicTable
            topicTable.Add(entry.Value, New SkillTypeTopicDescriptor(entry.Value, entry.Key))
        Next
        Return topicTable
    End Function
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of Topic, ITopic) = CreateDescriptors()
    <Extension>
    Public Function ToDescriptor(topic As Topic) As ITopic
        Return Descriptors(topic)
    End Function

End Module
