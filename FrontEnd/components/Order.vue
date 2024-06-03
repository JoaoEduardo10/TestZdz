<template>
  <v-main id="inspire">
    <v-content>
      <v-container fluid fill-height>
        <v-layout align-center justify-center>
          <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-toolbar dark color="primary">
                <v-btn v-if="isEditing" icon @click="goBack">
                  <v-icon>mdi-arrow-left</v-icon>
                </v-btn>
                <v-toolbar-title
                  >{{
                    isEditing ? "Editar" : "Adicionar"
                  }}
                  Pedido</v-toolbar-title
                >
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
                <v-btn color="primary" @click="submitForm">{{
                  isEditing ? "Salvar" : "Cadastrar"
                }}</v-btn>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
  </v-main>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";

export default defineComponent({
  name: "OrderForm",
  props: {
    isEditing: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      selectedProduct: 0,
      quantity: 0,
      products: [],
      apiUrl: this.$config.url_base,
      orderId: 0,
    };
  },
  methods: {
    async fetchProducts() {
      try {
        const response = await fetch(`${this.apiUrl}/product`);
        const data = await response.json();

        if (data.success) {
          this.products = data.result;
        } else {
          console.error("Erro ao buscar produtos:", data.message);
        }
      } catch (error: any) {
        console.error("Erro ao buscar produtos:", error.message);
      }
    },

    goBack() {
      this.$router.go(-1);
    },

    async fetchOrder() {
      if (!this.isEditing || !this.orderId) return;

      try {
        const response = await fetch(`${this.apiUrl}/order/${this.orderId}`);
        const data = await response.json();

        if (data.success) {
          const order = data.result;
          this.quantity = order.quantity;
          this.selectedProduct = order.product.id;
        } else {
          console.error("Erro ao buscar pedido:", data.ErrorMessage.Message);
        }
      } catch (error: any) {
        console.error("Erro ao buscar pedido:", error.message);
      }
    },
    async submitForm() {
      if (!this.selectedProduct || this.quantity <= 0) {
        alert("Selecione um produto e insira uma quantidade válida.");
        return;
      }

      const orderData = {
        produtoId: this.selectedProduct,
        quantity: Number(this.quantity),
      };

      try {
        const url = this.isEditing
          ? `${this.apiUrl}/order/${this.orderId}`
          : `${this.apiUrl}/order`;
        const method = this.isEditing ? "PUT" : "POST";

        const response = await fetch(url, {
          method,
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(orderData),
        });

        if (response.ok) {
          alert(
            `${
              this.isEditing ? "Pedido atualizado" : "Pedido cadastrado"
            } com sucesso!`
          );

          if (!this.isEditing) {
            this.selectedProduct = 0;
            this.quantity = 0;
          }
          return;
        }

        const data = await response.json();
        alert(
          `Erro ao ${this.isEditing ? "atualizar" : "cadastrar"} pedido: ${
            data.ErrorMessage.Message
          }`
        );
      } catch (error) {
        console.error(
          `Erro ao ${this.isEditing ? "atualizar" : "cadastrar"} pedido:`,
          error
        );
        alert(
          `Erro ao ${
            this.isEditing ? "atualizar" : "cadastrar"
          } pedido. Por favor, tente novamente mais tarde.`
        );
      }
    },
  },
  mounted() {
    this.orderId = parseInt(this.$route.params.id);
    this.fetchOrder();
    this.fetchProducts();
  },
});
</script>
