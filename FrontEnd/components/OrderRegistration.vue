<template>
  <v-app id="inspire">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-toolbar-title>Adicionar uma entrega</v-toolbar-title>
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
                <v-btn color="primary" @click="submitOrder">Cadastrar</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-app>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";
import { ApiResponse } from "~/interfaces/apiResponse";
import { Product } from "~/interfaces/product";

export default defineComponent({
  name: "OrderRegistration",
  setup() {
    const products = ref<Product[]>([]);
    const selectedProduct = ref<number | null>(null);
    const quantity = ref<number>(0);

    onMounted(async () => {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product");

        const data: ApiResponse<Product[]> = await response.json();

        if (data.success) {
          products.value = data.result;
        } else {
          console.error("Erro ao buscar produtos:", data.message);
        }
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
      }
    });

    const submitOrder = async () => {
      if (selectedProduct.value && quantity.value < 0) {
        alert("Selecione um produto e insira uma quantidade válida.");
        return;
      }

      const product = products.value.find(
        (p) => p.id === selectedProduct.value
      );

      if (!product) {
        alert("Produto não encontrado.");
        return;
      }

      const orderData = {
        produtoId: selectedProduct.value,
        quantity: quantity.value,
        value: product.value * quantity.value,
      };

      try {
        const response = await fetch("http://localhost:5042/api/v1/order", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(orderData),
        });

        if (response.status == 204) {
          alert("Pedido Registrado com sucesso!");
          return;
        }

        const data: ApiResponse<null> = await response.json();

        alert(`Error: ${data.errorMessage.Message}`);
      } catch (error: any) {
        console.error("Erro ao criar ordem:", error.Message);
        alert("Erro ao criar ordem. Por favor, tente novamente mais tarde.");
      }
    };

    return {
      products,
      selectedProduct,
      quantity,
      submitOrder,
    };
  },
});
</script>
