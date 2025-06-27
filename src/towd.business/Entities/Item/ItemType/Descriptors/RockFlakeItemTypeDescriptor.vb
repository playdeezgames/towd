Friend Class RockFlakeItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.RockFlake, "Rock Flake", True, "A jagged shard of stone, chipped away during the knapping of a sharp rock into a blade. 
This brittle byproduct is rough, irregular, and barely useful—more likely to cut your hand than anything else. 
Still, in the desperate wasteland of TOWD, you might find a use for it, like scraping hides or shanking something small. 
Handle with care, or don’t. 
Pain builds character.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
