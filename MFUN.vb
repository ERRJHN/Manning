'Imports LibOptimization.Optimization
'Imports LibOptimization.Base
'Imports System.Math


'' ==============================================================================
'' Classe de Função Objetivo para a Otimização (herda de absObjectiveFunction)
'' ==============================================================================
'Public Class ManningObjective
'    Inherits absObjectiveFunction

'    ' Parâmetros fixos do problema
'    Private Q As Double         ' Vazão (m³/s)
'    Private S As Double         ' Declividade de fundo (m/m)
'    Private nManning As Double  ' Coeficiente de Manning
'    Private m_ModeMax As Boolean ' Se True, otimiza para máxima velocidade (retorna -V)

'    ' Construtor: define os parâmetros fixos e o modo de otimização
'    Public Sub New(discharge As Double, slope As Double, mannCoeff As Double, maximize As Boolean)
'        Q = discharge
'        S = slope
'        nManning = mannCoeff
'        m_ModeMax = maximize
'    End Sub

'    ' Função objetivo principal
'    ' x(0) = b (base), x(1) = n (talude esquerdo), x(2) = m (talude direito)
'    Public Overrides Function F(x As List(Of Double)) As Double
'        Dim b As Double = x(0)
'        Dim leftSlope As Double = x(1)
'        Dim rightSlope As Double = x(2)

'        ' Resolve a equação de Manning para encontrar a profundidade y
'        Dim yFlow As Double = SolveDepth(b, leftSlope, rightSlope)
'        If yFlow <= 0 Then Return Double.MaxValue

'        ' Calcula a área da seção trapezoidal: A = (2*b*y + (n+m)*y²)/2
'        Dim A As Double = (2 * b * yFlow + (leftSlope + rightSlope) * yFlow ^ 2) / 2
'        If A <= 0 Then Return Double.MaxValue

'        ' Calcula a velocidade: V = Q / A (ou, alternativamente, pela fórmula de Manning)
'        Dim V As Double = Q / A

'        ' Se o modo é maximizar, retorna -V (para que a minimização encontre o maior V)
'        If m_ModeMax Then
'            Return -V
'        Else
'            Return V
'        End If
'    End Function

'    ' Número de variáveis de projeto = 3 (b, n, m)
'    Public Overrides Function NumberOfVariable() As Integer
'        Return 3
'    End Function

'    ' ============================
'    ' Método para resolver a equação de Manning (encontrar y)
'    ' ============================
'    Private Function SolveDepth(b As Double, leftSlope As Double, rightSlope As Double) As Double
'        Dim tol As Double = 0.000001
'        Dim maxIter As Integer = 100
'        Dim low As Double = 0.000001
'        Dim high As Double = 10.0   ' chute inicial para y máximo; se necessário será ajustado
'        Dim mid As Double = 0.0

'        Dim f_low As Double = FunctionF_y(low, b, leftSlope, rightSlope)
'        Dim f_high As Double = FunctionF_y(high, b, leftSlope, rightSlope)
'        ' Caso f(high) não seja positivo, aumente high até que fique positivo (ou até um limite)
'        While f_high < 0 And high < 1000
'            high *= 2
'            f_high = FunctionF_y(high, b, leftSlope, rightSlope)
'        End While

'        For iter As Integer = 1 To maxIter
'            mid = (low + high) / 2
'            Dim f_mid As Double = FunctionF_y(mid, b, leftSlope, rightSlope)
'            If Abs(f_mid) < tol Then
'                Return mid
'            End If
'            ' Se o sinal em low e mid são opostos, a raiz está entre low e mid
'            If f_low * f_mid < 0 Then
'                high = mid
'            Else
'                low = mid
'                f_low = f_mid
'            End If
'        Next
'        Return mid
'    End Function

'    ' Função auxiliar: f(y) = (1/nManning)*A(y)*[R(y)]^(2/3)*sqrt(S) - Q
'    ' onde A(y) = (2*b*y + (leftSlope+rightSlope)*y²)/2
'    '       P(y) = b + y*(sqrt(1+leftSlope²) + sqrt(1+rightSlope²))
'    '       R = A/P
'    Private Function FunctionF_y(y As Double, b As Double, leftSlope As Double, rightSlope As Double) As Double
'        Dim A As Double = (2 * b * y + (leftSlope + rightSlope) * y ^ 2) / 2
'        Dim P As Double = b + y * (Sqrt(1 + leftSlope ^ 2) + Sqrt(1 + rightSlope ^ 2))
'        Dim R As Double = A / P
'        Dim lhs As Double = (1 / nManning) * A * Math.Pow(R, 2.0 / 3.0) * Sqrt(S)
'        Return lhs - Q
'    End Function
'End Class

'' ==============================================================================
'' Módulo Principal: Executa duas otimizações (mínima e máxima velocidade)
'' ==============================================================================
'Module Program
'    Sub Main()
'        ' -------------------------------
'        ' Parâmetros fixos do canal
'        ' -------------------------------
'        Dim Q As Double = 10.0         ' Vazão em m³/s (exemplo)
'        Dim S As Double = 0.001        ' Declividade de fundo (m/m)
'        Dim nManning As Double = 0.015 ' Coeficiente de Manning

'        ' -------------------------------
'        ' Limites para as variáveis de projeto
'        ' -------------------------------
'        Dim B1 As Double = 2.0  ' base mínima (m)
'        Dim B2 As Double = 5.0  ' base máxima (m)
'        Dim n1 As Double = 1.0  ' talude esquerdo mínimo
'        Dim n2 As Double = 2.5  ' talude esquerdo máximo
'        Dim m1 As Double = 1.0  ' talude direito mínimo
'        Dim m2 As Double = 3.0  ' talude direito máximo

'        ' ================================
'        ' Otimização para MÍNIMA velocidade
'        ' ================================
'        Console.WriteLine("Otimização para MÍNIMA velocidade:")
'        ' Cria a função objetivo para minimização (retorna V)
'        Dim minObjective As New ManningObjective(Q, S, nManning, False)
'        Dim optMin As New clsOptDE(minObjective)
'        optMin.LowerBounds = New Double() {B1, n1, m1}
'        optMin.UpperBounds = New Double() {B2, n2, m2}
'        optMin.Init()
'        optMin.DoIteration()

'        Dim resMin = optMin.Result
'        Dim b_min As Double = resMin.Vector(0)
'        Dim leftSlope_min As Double = resMin.Vector(1)
'        Dim rightSlope_min As Double = resMin.Vector(2)
'        Dim y_min As Double = SolveDepthWrapper(b_min, leftSlope_min, rightSlope_min, Q, S, nManning)
'        Dim A_min As Double = (2 * b_min * y_min + (leftSlope_min + rightSlope_min) * y_min ^ 2) / 2
'        Dim V_min As Double = Q / A_min
'        Console.WriteLine($"Base = {b_min:F2} m, Talude Esquerdo = {leftSlope_min:F2}, Talude Direito = {rightSlope_min:F2}")
'        Console.WriteLine($"Profundidade = {y_min:F4} m, Velocidade = {V_min:F4} m/s")
'        Console.WriteLine("--------------------------------------------------------")

'        ' ================================
'        ' Otimização para MÁXIMA velocidade
'        ' ================================
'        Console.WriteLine("Otimização para MÁXIMA velocidade:")
'        ' Cria a função objetivo para maximização (internamente retorna -V)
'        Dim maxObjective As New ManningObjective(Q, S, nManning, True)
'        Dim optMax As New clsOptDE(maxObjective)
'        optMax.LowerBounds = New Double() {B1, n1, m1}
'        optMax.UpperBounds = New Double() {B2, n2, m2}
'        optMax.Init()
'        optMax.DoIteration()

'        Dim resMax = optMax.Result
'        Dim b_max As Double = resMax.Vector(0)
'        Dim leftSlope_max As Double = resMax.Vector(1)
'        Dim rightSlope_max As Double = resMax.Vector(2)
'        Dim y_max As Double = SolveDepthWrapper(b_max, leftSlope_max, rightSlope_max, Q, S, nManning)
'        Dim A_max As Double = (2 * b_max * y_max + (leftSlope_max + rightSlope_max) * y_max ^ 2) / 2
'        Dim V_max As Double = Q / A_max
'        Console.WriteLine($"Base = {b_max:F2} m, Talude Esquerdo = {leftSlope_max:F2}, Talude Direito = {rightSlope_max:F2}")
'        Console.WriteLine($"Profundidade = {y_max:F4} m, Velocidade = {V_max:F4} m/s")

'        ' Aguarda para visualizar os resultados
'        Console.WriteLine(vbCrLf & "Pressione qualquer tecla para sair...")
'        Console.ReadKey()
'    End Sub

'    ' -------------------------------------------------------
'    ' Wrapper para calcular a profundidade (equação de Manning)
'    ' Esse procedimento replica a lógica de SolveDepth da classe
'    ' -------------------------------------------------------
'    Private Function SolveDepthWrapper(b As Double, leftSlope As Double, rightSlope As Double, Q As Double, S As Double, nManning As Double) As Double
'        Dim tol As Double = 0.000001
'        Dim maxIter As Integer = 100
'        Dim low As Double = 0.000001
'        Dim high As Double = 10.0
'        Dim mid As Double = 0.0

'        Dim f_low As Double = FunctionFWrapper(low, b, leftSlope, rightSlope, Q, S, nManning)
'        Dim f_high As Double = FunctionFWrapper(high, b, leftSlope, rightSlope, Q, S, nManning)
'        While f_high < 0 And high < 1000
'            high *= 2
'            f_high = FunctionFWrapper(high, b, leftSlope, rightSlope, Q, S, nManning)
'        End While

'        For iter As Integer = 1 To maxIter
'            mid = (low + high) / 2
'            Dim f_mid As Double = FunctionFWrapper(mid, b, leftSlope, rightSlope, Q, S, nManning)
'            If Abs(f_mid) < tol Then
'                Return mid
'            End If
'            If f_low * f_mid < 0 Then
'                high = mid
'            Else
'                low = mid
'                f_low = f_mid
'            End If
'        Next
'        Return mid
'    End Function

'    ' Função auxiliar para o wrapper: f(y) = (1/nManning)*A(y)*[R(y)]^(2/3)*sqrt(S) - Q
'    Private Function FunctionFWrapper(y As Double, b As Double, leftSlope As Double, rightSlope As Double, Q As Double, S As Double, nManning As Double) As Double
'        Dim A As Double = (2 * b * y + (leftSlope + rightSlope) * y ^ 2) / 2
'        Dim P As Double = b + y * (Sqrt(1 + leftSlope ^ 2) + Sqrt(1 + rightSlope ^ 2))
'        Dim R As Double = A / P
'        Dim lhs As Double = (1 / nManning) * A * Math.Pow(R, 2.0 / 3.0) * Sqrt(S)
'        Return lhs - Q
'    End Function
'End Module
