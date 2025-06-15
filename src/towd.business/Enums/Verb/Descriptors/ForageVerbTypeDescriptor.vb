Imports towd.data

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Forage, "Forage", 1)
    End Sub

    Private ReadOnly forageTable As IReadOnlyDictionary(Of LocationType, ItemType) =
        New Dictionary(Of LocationType, ItemType) From
        {
            {LocationType.Grass, ItemType.PlantFiber},
            {LocationType.Pine, ItemType.Stick},
            {LocationType.Rock, ItemType.Rock}
        }

    Protected Overrides Sub OnPerform(character As ICharacter)
        Dim world = character.World
        character.AddMessage("You forage.")
        Dim location = character.Location
        location.ChangeStatistic(StatisticType.ForagingCounter, -1)
        Dim itemType = forageTable(character.Location.EntityType.LocationType).ToDescriptor
        Dim itemCount = RNG.GenerateInclusiveRange(1, character.GetStatistic(StatisticType.ForagingSkill))
        For Each dummy In Enumerable.Range(0, itemCount)
            Dim item = world.CreateItem(itemType)
            character.AddItem(item)
        Next
        character.AppendMessage($"+{itemCount} {itemType.Name}(total {character.GetCountOfItemType(itemType)})")
        character.ChangeStatistic(StatisticType.ForagingCounter, 1)
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.GetStatistic(data.StatisticType.ForagingCounter) > 0
    End Function

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return character.Location.GetStatistic(StatisticType.ForagingCounter)
    End Function
End Class
