Imports Shouldly
Imports towd.data
Imports Xunit

Namespace towd.business.tests
    Public Class Enumeration_Values_Should_Have_Descriptors_For
        <Fact>
        Sub Statistic_Types()
            For Each statisticType In [Enum].GetValues(Of StatisticType)
                Try
                    statisticType.ToDescriptor.ShouldNotBeNull($"{statisticType}'s Descriptor Should Not Be Null")
                Catch ex As Exception
                    Assert.Fail($"{statisticType}'s Descriptor Should Exist")
                End Try
            Next
        End Sub
    End Class
End Namespace

