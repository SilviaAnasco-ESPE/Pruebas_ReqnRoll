Feature: PedidoEditar

A short summary of the feature

@tag1
Scenario: Editar pedido exitosamente
	Given Usuario hace clic en el botón de editar de un pedido de id: 1
	When Cambia los valores del formulario con
	| ClienteId | Monto | Estado    |
	| 9        | 12,36  | Completado |
	And Clic en el boton de Editar Pedido
	Then Se redirige a la Lista
	And El pedido aparece editado en la lista
	| PedidoID | ClienteId | Monto | Estado    |
	| 1        | 9        | 12,36  | Completado |

@tag2
Scenario: Editar pedido dejando campos vacios
	Given Usuario hace clic en el botón de editar de un pedido de id: 1
	When Deja los campos vacios
	And Clic en el boton de Editar Pedido
	Then Permanece en la pagina de Edicion
	And Se muestra el error "The value '' is invalid."
