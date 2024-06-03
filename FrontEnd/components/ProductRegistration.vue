<template>
  <v-app id="inspire">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Editar Pedido</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form ref="form">
                  <v-select
                    v-model="selectedProduct"
                    label="Produto"
                    :items="productOptions"
                    item-text="name"
                    item-value="id"
                  ></v-select>
                  <v-text-field
                    v-model="quantity"
                    id="quantity"
                    name="quantity"
                    label="Quantidade"
                    type="number"
                  ></v-text-field>
                </v-form>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="updateOrder">Salvar</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import { ref, onMounted } from "vue";

export default {
  setup() {
    const selectedProductId = ref(null);
    const quantity = ref(0);
    const productName = ref("");
    const productOptions = ref([]);

    const fetchOrder = async () => {
      try {
        selectedProductId.value = $nuxt.$route.params.id;

        const response = await fetch(
          `http://localhost:5042/api/v1/order/${selectedProductId.value}`
        );

        const data = await response.json();

        if (data.success) {
          const order = data.result;
          productName.value = order.productName; // Assuming productName comes from the order data
          quantity.value = order.quantity;
        } else {
          console.error("Erro ao buscar pedido:", data.message);
        }
      } catch (error) {
        console.error("Erro ao buscar pedido:", error);
      }
    };

    const updateOrder = async () => {
      if (quantity.value <= 0) {
        alert("Insira uma quantidade válida.");
        return;
      }

      try {
        const response = await fetch(
          `http://localhost:5042/api/v1/order/${selectedProductId.value}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              quantity: quantity.value,
            }),
          }
        );

        if (response.ok) {
          alert("Pedido atualizado com sucesso!");
          // Redirecionar ou realizar outra ação após a atualização
        } else {
          const data = await response.json();
          alert(`Erro ao atualizar pedido: ${data.ErrorMessage.Message}`);
        }
      } catch (error) {
        console.error("Erro ao atualizar pedido:", error.Message);
        alert(
          "Erro ao atualizar pedido. Por favor, tente novamente mais tarde."
        );
      }
    };

    onMounted(() => {
      fetchOrder();
      fetchProductOptions();
    });

    const fetchProductOptions = async () => {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product");
        const data = await response.json();
        if (data.success) {
          productOptions.value = data.result;
        } else {
          console.error("Erro ao buscar produtos:", data.message);
        }
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
      }
    };

    return {
      selectedProductId,
      quantity,
      productName,
      productOptions,
      updateOrder,
    };
  },
};
</script>
