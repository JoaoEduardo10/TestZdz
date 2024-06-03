<template>
  <v-app id="inspire">
    <v-main>
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
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { ApiResponseError } from "~/interfaces/apiResponseError";
import { Product } from "~/interfaces/product";

export default defineComponent({
  name: "OrderRegistration",
  data() {
    return {
      products: [] as Product[],
      selectedProduct: 0,
      quantity: 0,
    };
  },
  methods: {
    async getAllProducts() {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product");
        const data = await response.json();

        if (data.success) {
          this.products = data.result;
        } else {
          console.error("Erro ao buscar produtos:", data.ErrorMessage.Message);
        }
      } catch (error: any) {
        console.error("Erro ao buscar produtos:", error.message);
      }
    },
    async submitOrder() {
      if (this.selectedProduct <= 0 && this.quantity <= 0) {
        alert("Selecione um produto e insira uma quantidade válida.");
        return;
      }

      const product = this.products.find(
        (p: Product) => p.id === this.selectedProduct
      );

      if (!product) {
        alert("Produto não encontrado.");
        return;
      }

      const orderData = {
        productId: this.selectedProduct,
        quantity: this.quantity,
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

        const data: ApiResponseError<null> = await response.json();

        alert(`Error: ${data.ErrorMessage.Message}`);
      } catch (error: any) {
        console.error("Erro ao criar ordem:", error.message);
        alert("Erro ao criar ordem. Por favor, tente novamente mais tarde.");
      }
    },
  },
  mounted() {
    this.getAllProducts();
  },
});
</script>
