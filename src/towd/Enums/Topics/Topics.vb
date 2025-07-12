Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports towd.business

Public Module Topics
    Public ReadOnly SkillTypeTopicTable As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {
            {SkillType.Foraging, Topic.SkillTypeForaging},
            {SkillType.Dig, Topic.SkillTypeDig},
            {SkillType.Knapping, Topic.SkillTypeKnapping},
            {SkillType.Chop, Topic.SkillTypeChop},
            {SkillType.Fish, Topic.SkillTypeFish}
        }
    Public ReadOnly VerbTypeTopicTable As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {
            {VerbType.Twine, Topic.VerbTypeTwine},
            {VerbType.SharpRock, Topic.VerbTypeSharpRock},
            {VerbType.SharpStick, Topic.VerbTypeSharpStick},
            {VerbType.Hatchet, Topic.VerbTypeHatchet},
            {VerbType.Hammer, Topic.VerbTypeHammer},
            {VerbType.Plank, Topic.VerbTypePlank},
            {VerbType.CookingFire, Topic.VerbTypeCookingFire},
            {VerbType.Furnace, Topic.VerbTypeFurnace},
            {VerbType.CharcoalFromCookingFire, Topic.VerbTypeCharcoalFromCookingFire},
            {VerbType.CharcoalFromFurnace, Topic.VerbTypeCharcoalFromFurnace},
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
            {VerbType.AddFuelFurnaceCharcoal, Topic.VerbTypeAddFuelFurnaceCharcoal},
            {VerbType.EatCarrot, Topic.VerbTypeEatCarrot}
        }
    Public Function ToItemTypeTopic(itemType As String) As String
        Return $"ItemType{itemType}"
    End Function
    Public Function ToVerbTypeTopic(verbType As String) As String
        Return $"VerbType{verbType}"
    End Function
    Private Sub AddEnumTopic(Of TEnum)(
                                      topicTable As Dictionary(Of String, ITopic),
                                      toTopic As Func(Of String, String),
                                      toDescriptor As Func(Of String, String, ITopic))
        Dim enumTypeModule = GetType(TEnum)
        Dim enumTypes = enumTypeModule.GetFields(BindingFlags.Public Or BindingFlags.Static)
        For Each enumType In enumTypes
            Dim enumTypeValue = CStr(enumType.GetRawConstantValue())
            Dim topic = toTopic(enumTypeValue)
            topicTable.Add(topic, toDescriptor(topic, enumTypeValue))
        Next
    End Sub

    Private Function CreateDescriptors() As IReadOnlyDictionary(Of String, ITopic)
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
            New TopicDescriptor(Topic.NavigationVerb, "Verbs", "Actions speak louder than groans. 
Forage, dig, chop—choose your labor and test your skills against the wild’s whims."),
            New TopicDescriptor(Topic.NavigationMap, "Map", "You thought you could just wander this godforsaken wasteland and not get lost? Ha! The Map command is your pathetic attempt to make sense of the desolate hellscape you're barely surviving in. Pull it up from the navigation screen to see where you are—or more likely, to confirm you're nowhere near anything that won't kill you. It shows your sorry position, nearby landmarks (if you can call a pile of bones a ""landmark""), and maybe a hint of what fresh misery lies ahead. Use it to plot your next move, but don’t expect it to hold your hand—freedom means figuring this crap out yourself."),
            New TopicDescriptor(Topic.NavigationStatistics, "Statistics", "This is the place to see yer stats! You can then contemplate yer life choices. Or don't. You do you."),
            New TopicDescriptor(Topic.NavigationDialog, "Dialog", "This is where you can dialog with another character in the game. Yes, I'm using the world ""Dialog"" as a verb here. I know its a noun. I blame HR.")
        }.ToDictionary(Function(x) x.Topic, Function(x) x)
        AddEnumTopic(Of ItemType)(topicTable, AddressOf ToItemTypeTopic, AddressOf ItemTypeTopicDescriptor.Create)
        For Each entry In VerbTypeTopicTable
            topicTable.Add(entry.Value, New VerbTypeTopicDescriptor(entry.Value, entry.Key))
        Next
        For Each entry In SkillTypeTopicTable
            topicTable.Add(entry.Value, New SkillTypeTopicDescriptor(entry.Value, entry.Key))
        Next
        Return topicTable
    End Function
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of String, ITopic) = CreateDescriptors()
    <Extension>
    Public Function ToTopicDescriptor(topic As String) As ITopic
        Return Descriptors(topic)
    End Function
End Module
