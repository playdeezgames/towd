Imports System.Runtime.CompilerServices

Module Topics
    Public ReadOnly Descriptors As IReadOnlyDictionary(Of Topic, ITopic) =
        New List(Of ITopic) From
        {
            New TopicDescriptor(Topic.NavigationDeeds, "Deeds", "This option will let you look at yer ""Deeds"", which are achievements you have accomplished within a run."),
            New TopicDescriptor(Topic.NavigationGameMenu, "Game Menu", "This option will let you save, load, or abandon the game."),
            New TopicDescriptor(Topic.NavigationInventory, "Inventory", "This option will let you examine the items in yer inventory."),
            New TopicDescriptor(Topic.NavigationMove, "Movement", "This option will allow you to move yer character."),
            New TopicDescriptor(Topic.NavigationSkills, "Skills", "This option will allow you to manage yer skills."),
            New TopicDescriptor(Topic.NavigationVerb, "Verbs", "This option will allow you to perform actions within the game world.")
        }.ToDictionary(Function(x) x.Topic, Function(x) x)
    <Extension>
    Public Function ToDescriptor(topic As Topic) As ITopic
        Return Descriptors(topic)
    End Function

End Module
