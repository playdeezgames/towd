Friend Class Character
    Implements ICharacter

    Private worldData As data.WorldData
    Private characterId As Integer

    Public Sub New(worldData As data.WorldData, characterId As Integer)
        Me.worldData = worldData
        Me.characterId = characterId
    End Sub

    Public ReadOnly Property Id As Integer Implements ICharacter.Id
        Get
            Return characterId
        End Get
    End Property
End Class
