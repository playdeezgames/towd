Imports System.Runtime.CompilerServices

Friend Module RNG
    Private ReadOnly random As New Random()
    <Extension>
    Friend Function Generate(Of TGenerated)(table As IReadOnlyDictionary(Of TGenerated, Integer)) As TGenerated
        Dim generated = random.Next(table.Values.Sum)
        For Each item In table
            generated -= item.Value
            If generated < 0 Then
                Return item.Key
            End If
        Next
        Throw New ArgumentException("you should not get here")
    End Function

    Friend Function GenerateInclusiveRange(minimum As Integer, maximum As Integer) As Integer
        Return random.Next(minimum, maximum + 1)
    End Function
End Module
