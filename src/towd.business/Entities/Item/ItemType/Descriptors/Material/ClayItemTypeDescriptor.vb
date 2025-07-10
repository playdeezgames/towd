Friend Class ClayItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Clay, "Clay", True, "Muddy earth ready to mold. 
Shape it into bricks or pots—its potential awaits the fire.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
