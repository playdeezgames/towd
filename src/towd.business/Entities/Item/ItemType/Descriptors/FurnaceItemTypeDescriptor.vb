Friend Class FurnaceItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.Furnace, "Furnace", True, "A roaring fire pit. 
Smelt clay or forge tools—master it, and the Wastes bend to you.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
