Friend Class BrickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Brick, "Brick", True, "A fired clay stronghold. 
Build walls or barter it—its durability is your defense.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
