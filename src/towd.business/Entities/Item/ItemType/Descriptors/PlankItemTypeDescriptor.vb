Friend Class PlankItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(business.ItemType.Plank, "Plank", True, "A smoothed wooden slab. 
Build shelter or barter it—its utility grows with your skill.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
