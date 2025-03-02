Friend Class BladeItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Blade, "Blade")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
