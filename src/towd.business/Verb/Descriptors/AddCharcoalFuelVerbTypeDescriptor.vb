Friend Class AddCharcoalFuelVerbTypeDescriptor
    Inherits AddFuelVerbTypeDescriptor
    Public Sub New()
        MyBase.New(
            VerbType.AddCharcoalFuel,
            "Add Charcoal Fuel",
            0,
            16,
            data.ItemType.Charcoal.ToDescriptor,
            {data.LocationType.Furnace})
    End Sub
End Class
