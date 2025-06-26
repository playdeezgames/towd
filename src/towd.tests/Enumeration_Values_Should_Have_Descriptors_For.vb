Imports Shouldly
Imports Xunit

Namespace towd.tests
    Public Class Enumeration_Values_Should_Have_Descriptors_For
        <Fact>
        Sub Topics()
            For Each topic In [Enum].GetValues(Of Topic)
                Try
                    topic.ToDescriptor.ShouldNotBeNull($"{topic}'s Descriptor Should Not Be Null")
                Catch ex As Exception
                    Assert.Fail($"{topic}'s Descriptor Should Exist")
                End Try
            Next
        End Sub
    End Class
End Namespace

