Imports towd.data

Friend MustInherit Class AddFuelVerbTypeDescriptor
    Inherits VerbTypeDescriptor
    Private ReadOnly fuelDelta As Integer
    Private ReadOnly itemType As IItemType
    Private ReadOnly locationTypes As New HashSet(Of data.LocationType)
    Protected Sub New(
                     verbType As VerbType,
                     name As String,
                     timeTaken As Integer,
                     fuelDelta As Integer,
                     itemType As IItemType,
                     locationTypes As data.LocationType())
        MyBase.New(verbType, name, timeTaken)
        Me.fuelDelta = fuelDelta
        Me.itemType = itemType
        For Each locationType In locationTypes
            Me.locationTypes.Add(locationType)
        Next
    End Sub
    Protected Overrides Sub OnPerform(character As ICharacter)
        character.Location.ChangeStatistic(data.StatisticType.Fuel, fuelDelta)
        character.AppendMessage($"{If(fuelDelta > 0, "+", "")}{fuelDelta} Fuel(x{character.Location.GetStatistic(data.StatisticType.Fuel)})")
        Dim item = character.GetItemsOfType(ItemType).First
        character.RemoveItem(item)
        character.AppendMessage($"-1 {ItemType.Name}({character.GetCountOfItemType(ItemType)} remaining)")
        item.Recycle()
    End Sub
    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return locationTypes.Contains(character.Location.EntityType.LocationType) AndAlso character.GetCountOfItemType(itemType) > 0
    End Function
End Class
