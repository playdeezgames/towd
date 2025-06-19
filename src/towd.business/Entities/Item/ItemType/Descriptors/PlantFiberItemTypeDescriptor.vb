Friend Class PlantFiberItemTypeDescriptor
    Inherits ItemTypeDescriptor

    Public Sub New()
        MyBase.New(data.ItemType.PlantFiber, "Plant Fiber", True, "Tough strands from the wild. 
Weave them into twine or use as kindling—fragile alone, but vital in the right hands.")
    End Sub

    Public Overrides Sub Initialize(item As IItem)
    End Sub

    Public Overrides Sub AdvanceTime(item As IItem, amount As Integer)
    End Sub
End Class
