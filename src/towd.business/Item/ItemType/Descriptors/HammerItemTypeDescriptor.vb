Friend Class HammerItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Hammer, "Hammer")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
