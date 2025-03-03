Imports towd.data

Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Forage, "Forage")
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
        location.ChangeStatistic(StatisticType.Foraging, -1)
        Dim itemType = forageTable(character.Location.EntityType.LocationType).ToDescriptor
        Dim item = world.CreateItem(itemType)
        character.AddItem(item)
        character.AppendMessage($"+1 {itemType.Name}(total {character.GetCountOfItemType(itemType)})")
        world.AdvanceTime(1)
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.GetStatistic(data.StatisticType.Foraging) > 0
    End Function
End Class
