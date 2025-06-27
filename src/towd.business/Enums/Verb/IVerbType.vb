Public Interface IVerbType
    ReadOnly Property VerbType As VerbType
    Function CanCraft(character As ICharacter) As Boolean
    Sub Craft(character As ICharacter)
    ReadOnly Property Name As String
    ReadOnly Property Description As String
End Interface
