<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-flex xs12>
        <v-card class="elevation-12">
          <v-card-title>Lista de Produtos</v-card-title>
          <v-data-table
            :items="formattedProducts"
            :headers="headers"
            :items-per-page="5"
          >
            <template v-slot:item.edit="{ item }">
              <v-btn class="blue" icon>
                <NuxtLink :to="`product/${item.id}`" class="white--text">
                  <v-icon>mdi-pencil</v-icon>
                </NuxtLink>
              </v-btn>
            </template>
            <template v-slot:item.delete="{ item }">
              <v-btn class="red" icon @click="deleteProduct(item.id)">
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </template>
          </v-data-table>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import { defineComponent, computed } from "vue";
import { Product } from "~/interfaces/product";

export default defineComponent({
  name: "ListProducts",
  data() {
    return {
      products: [] as Product[],
      headers: [
        { text: "Nome", value: "name" },
        { text: "Valor", value: "valueFormatted" },
        { text: "Editar", value: "edit", sortable: false },
        { text: "Excluir", value: "delete", sortable: false },
      ],
    };
  },
  computed: {
    formattedProducts(): Product[] {
      return this.products.map((product) => ({
        ...product,
        valueFormatted: this.formatCurrency(product.value),
      }));
    },
  },
  mounted() {
    this.getAllProducts();
  },
  methods: {
    async getAllProducts() {
      try {
        const response = await fetch("http://localhost:5042/api/v1/product");
        const data = await response.json();

        if (data.success) {
          this.products = data.result;
        } else {
          alert(`Erro: ${data.ErrorMessage.Message}`);
        }
      } catch (error) {
        alert(
          "Erro ao buscar produtos. Por favor, tente novamente mais tarde."
        );
      }
    },

    formatCurrency(value: number): string {
      return value.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL",
      });
    },

    async deleteProduct(id: number) {
      if (
        confirm(
          "Tem certeza que deseja excluir este produto? Também será apagado os pedidos relacionados a ele!"
        )
      ) {
        try {
          const response = await fetch(
            `http://localhost:5042/api/v1/product/${id}`,
            {
              method: "DELETE",
            }
          );

          if (response.ok) {
            alert("Produto excluído com sucesso!");
            this.getAllProducts();
          } else {
            const data = await response.json();
            alert(`Erro ao excluir produto: ${data.message}`);
          }
        } catch (error) {
          alert(
            "Erro ao excluir produto. Por favor, tente novamente mais tarde."
          );
        }
      }
    },
  },
});
</script>
