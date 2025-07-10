Friend Class TwineItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Twine, "Twine", True, "Rope twisted from plant fibers. 
Bind tools, mend gear, or rig a trap. 
Weak alone, but strong in purpose.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
