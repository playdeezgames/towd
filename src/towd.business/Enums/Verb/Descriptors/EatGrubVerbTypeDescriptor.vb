Friend Class EatGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatGrub, "Eat Grub", 0, data.ItemType.CookedGrub.ToDescriptor)
    End Sub

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return character.GetCountOfItemType(data.ItemType.CookedGrub.ToDescriptor)
    End Function
End Class
