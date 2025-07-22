Friend Class RangeCharacterWeightedGenerator
    Implements ICharacterWeightedGenerator

    Private ReadOnly minimum As Integer
    Private ReadOnly maximum As Integer

    Public Sub New(minimum As Integer, maximum As Integer)
        Me.minimum = minimum
        Me.maximum = maximum
    End Sub

    Public Function GetMinimum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMinimum
        Return minimum
    End Function

    Public Function GetMaximum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMaximum
        Return maximum
    End Function

    Public Function Generate(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.Generate
        Return RNG.GenerateInclusiveRange(GetMinimum(character), GetMaximum(character))
    End Function

    Public Overrides Function ToString() As String
        Return $"{minimum} - {maximum}"
    End Function
End Class
