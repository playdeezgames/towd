Friend Class EatGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatGrub, "Eat Grub", 0, data.ItemType.CookedGrub.ToDescriptor, "Satisfy your hunger with what you’ve found. 
Munch on grubs or scraps to restore satiety, but beware spoiled bites that sap your health. 
Choose wisely, famished wanderer.")
    End Sub

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return character.GetCountOfItemType(data.ItemType.CookedGrub.ToDescriptor)
    End Function
End Class
