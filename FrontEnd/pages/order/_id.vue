<template>
  <v-main id="inspire">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-btn icon @click="$router.push('/listOrder')">
                  <v-icon>mdi-arrow-left</v-icon>
                </v-btn>
                <v-toolbar-title>Editar Pedido</v-toolbar-title>
              </v-toolbar>
              <v-card-text>
                <v-form ref="form">
                  <v-select
                    v-model="selectedProduct"
                    label="Produto"
                    :items="products"
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
  </v-main>
</template>

<script>
import { defineComponent, ref, onMounted } from "vue";

export default defineComponent({
  name: "OrderEdit",
  setup() {
    const Id = ref(null);
    const selectedProduct = ref(0);
    const quantity = ref(0);
    const products = ref([]);

    const fetchOrder = async () => {
      try {
        Id.value = parseInt($nuxt.$route.params.id);

        const response = await fetch(
          `http://localhost:5042/api/v1/order/${Id.value}`
        );

        const data = await response.json();

        if (data.success) {
          const order = data.result;
          quantity.value = order.quantity;
          selectedProduct.value = data.result.product.id;
        } else {
          console.error("Erro ao buscar pedido:", data.message);
        }
      } catch (error) {
        console.error("Erro ao buscar pedido:", error);
      }
    };

    const fetchProducts = async () => {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product");

        const data = await response.json();

        if (data.success) {
          products.value = data.result;
        } else {
          console.error("Erro ao buscar produtos:", data.message);
        }
      } catch (error) {
        console.error("Erro ao buscar produtos:", error.message);
      }
    };

    const updateOrder = async () => {
      if (!selectedProduct.value || quantity.value <= 0) {
        alert("Selecione um produto e insira uma quantidade válida.");
        return;
      }

      const orderData = {
        produtoId: selectedProduct.value,
        quantity: Number(quantity.value),
      };

      try {
        const response = await fetch(
          `http://localhost:5042/api/v1/order/${Id.value}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(orderData),
          }
        );

        if (response.ok) {
          alert("Pedido atualizado com sucesso!");
          return;
        }

        const data = await response.json();
        alert(`Erro ao atualizar pedido: ${data.ErrorMessage.Message}`);
        return;
      } catch (error) {
        console.error("Erro ao atualizar pedido:", error.Message);
        alert(
          "Erro ao atualizar pedido. Por favor, tente novamente mais tarde."
        );
        return;
      }
    };

    onMounted(() => {
      fetchProducts();
      fetchOrder();
    });

    return {
      Id,
      quantity,
      products,
      updateOrder,
      selectedProduct,
    };
  },
});
</script>
