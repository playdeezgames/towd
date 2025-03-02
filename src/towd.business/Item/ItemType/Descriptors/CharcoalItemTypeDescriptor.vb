Friend Class CharcoalItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Charcoal, "Charcoal")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
