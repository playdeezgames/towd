Friend Class SharpStickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.SharpStick, "Sharp Stick")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
