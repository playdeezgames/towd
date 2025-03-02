Friend Class ClayItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Clay, "Clay")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
