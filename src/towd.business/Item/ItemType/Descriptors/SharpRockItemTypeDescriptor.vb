Friend Class SharpRockItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.SharpRock, "Sharp Rock")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
