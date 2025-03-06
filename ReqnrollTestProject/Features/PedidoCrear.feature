Feature: PedidoCrear

A short summary of the feature

@tag1
Scenario: Crear pedido exitosamente
	Given Usuario en la página de crear un pedido
	When Llena el formulario con
	| ClienteId | Monto | Estado    |
	| 10        | 2,35  | Pendiente |
	And Clic en el boton de Crear Pedido
	Then Se redirige a la Lista de Pedidos
	And El pedido aparece en la lista
	| ClienteId | Monto | Estado    |
	| 10        | 2,35  | Pendiente |

@tag2
  Scenario: Error al crear pedido
    Given Usuario se dirige a la página de crear un pedido
    When Llena el formulario con los valores
      | ClienteId | Monto | Estado    |
      | 3         | 2,35  | Pendiente | 
    And Clic en el boton Crear Pedido
    Then Permanece en la misma página de creación
    And Se muestra un mensaje de error "El ClienteID no existe."