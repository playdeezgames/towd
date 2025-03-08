Friend Class EatGrubVerbTypeDescriptor
    Inherits EatVerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.EatGrub, "Eat Grub", 0, data.ItemType.CookedGrub.ToDescriptor)
    End Sub
End Class
