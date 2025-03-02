Friend Class GrubItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Grub, "Grub")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
