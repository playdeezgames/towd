Friend Class EatFishVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatFish, "Eat Fish", 0, data.ItemType.CookedFishFilet.ToDescriptor)
    End Sub

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return character.GetCountOfItemType(data.ItemType.CookedFishFilet.ToDescriptor)
    End Function
End Class
