Friend Class EatFishVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatFish, "Eat Fish", 0, data.ItemType.CookedFishFilet.ToDescriptor, "Devour your catch to fill your belly. 
Fresh fish boosts satiety and health, but a bad haul could turn your stomach. 
Savor the victory—or spit it out.")
    End Sub

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return character.GetCountOfItemType(data.ItemType.CookedFishFilet.ToDescriptor)
    End Function
End Class
