Friend Class AddStickFuelVerbTypeDescriptor
    Inherits AddFuelVerbTypeDescriptor
    Public Sub New()
        MyBase.New(
            VerbType.AddStickFuel,
            "Add Stick Fuel",
            0,
            1,
            data.ItemType.Stick.ToDescriptor,
            {data.LocationType.CookingFire})
    End Sub
End Class
