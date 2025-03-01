Public Interface IVerbType
    ReadOnly Property VerbType As VerbType
    ReadOnly Property Name As String
    Function CanPerform(character As ICharacter) As Boolean
    Sub Perform(character As ICharacter)
End Interface
