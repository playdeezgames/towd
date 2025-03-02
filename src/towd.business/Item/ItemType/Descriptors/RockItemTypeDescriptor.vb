Friend Class RockItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Rock, "Rock")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub
End Class
