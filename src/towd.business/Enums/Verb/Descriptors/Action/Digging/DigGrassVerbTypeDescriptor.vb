﻿Friend Class DigGrassVerbTypeDescriptor
    Inherits DigVerbTypeDescriptor

    Public Sub New()
        MyBase.New(
            business.VerbType.DigGrass,
            "Dig(Grubs)",
            business.LocationType.Grass,
            business.ItemType.Grub)
    End Sub
End Class
