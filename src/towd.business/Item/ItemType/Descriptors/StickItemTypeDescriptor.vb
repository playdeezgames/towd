Friend Class StickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Stick, "Stick")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
