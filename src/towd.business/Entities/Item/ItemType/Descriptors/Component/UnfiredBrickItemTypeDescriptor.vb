Friend Class UnfiredBrickItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.UnfiredBrick, "Unfired Brick", True, "A damp clay block. 
Harden it into a brick, but handle with care—drop it, and it crumbles.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
